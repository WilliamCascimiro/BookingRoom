import { Component, OnInit } from '@angular/core';
import { RequestBooking } from 'src/app/interfaces/Request';
import { RoomDateTimeSlot } from 'src/app/interfaces/RoomTimeSlot';
import { BookingService } from 'src/app/services/booking/booking.service';
import { RoomService } from 'src/app/services/room/room.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { Room } from 'src/app/interfaces/Room';
import { DetailBookingResponse } from 'src/app/interfaces/booking/DetailBookingResponse';
import { UpdateBookingRequest } from 'src/app/interfaces/booking/UpdateBookingRequest';
import { AuthService } from 'src/app/services/Auth/auth.service';

@Component({
  selector: 'app-room-booking-edit',
  templateUrl: './room-booking-edit.component.html',
  styleUrls: ['./room-booking-edit.component.css']
})
export class RoomBookingEditComponent implements OnInit {
  rooms: Room[] = [];
  selectedRoom: string = '';
  selectedDate: Date | null = null;
  timeSlotList: RoomDateTimeSlot[] = [];
  response: DetailBookingResponse | undefined;
  bookingIdParameter: string = '';

  constructor(private roomService: RoomService, private authService: AuthService, private bookingService: BookingService, private snackBar: MatSnackBar, private route: ActivatedRoute) {}
  
  ngOnInit() {
    this.bookingIdParameter = this.route.snapshot.paramMap.get('id') ?? '';
    this.getRooms();
    this.getBooking();
  }
  
  isAnyTimeSlotSelected(): boolean {
    return this.timeSlotList.some(slot => slot.selected);
  }

  getBooking() {
    this.bookingService.getBooking(this.bookingIdParameter).subscribe(booking => {
      this.response = booking;
      this.selectedRoom = booking.roomId;
      this.selectedDate = new Date(`${booking.date}T00:00:00`);
      this.loadTimeSlots();
    });
  }

  getRooms() {
    this.roomService.getRooms().subscribe(rooms => {
      this.rooms = rooms;
    });
  }

  loadTimeSlots() {      
    const formattedDate = new Intl.DateTimeFormat('en-CA').format(this.selectedDate ?? new Date());
    this.roomService.getRoomDateTimeSlotsSelected(this.selectedRoom, formattedDate, this.bookingIdParameter).subscribe(timeSlots => {
      this.timeSlotList = timeSlots;
    });
  }

  saveSelection() {
    const itensSelecionados = this.timeSlotList.filter(item => item.selected).map(x => x.id);
    
    const updateBookingRequest: UpdateBookingRequest = {
      bookingId: this.bookingIdParameter,
      timeSlotsId: itensSelecionados,
      roomId: this.selectedRoom,
      userId: this.authService.getUserIdFromToken()
    };

    this.bookingService.updateBooking(updateBookingRequest).subscribe({
      next: (response) => {
        this.snackBar.open('Reserva atualizada com sucesso!', 'Fechar', {
          duration: 3000,
        });
        this.loadTimeSlots()
      },
      error: (err) => {
        if(err.error.detail == 'Os horários selecionados devem estar em sequência.') {
          this.snackBar.open('Os horários selecionados devem estar em sequência.', 'Fechar', {
            duration: 3000,
          });
        }
        else{
          this.snackBar.open('Erro ao atualizar a reserva. Tente novamente.', 'Fechar', {
            duration: 3000,
          });
        }
        this.loadTimeSlots()
      },
    });    
  }

}
