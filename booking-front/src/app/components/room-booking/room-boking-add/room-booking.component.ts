import { Component } from '@angular/core';
import { RequestBooking } from 'src/app/interfaces/Request';
import { RoomDateTimeSlot } from 'src/app/interfaces/RoomTimeSlot';
import { BookingService } from 'src/app/services/booking/booking.service';
import { RoomService } from 'src/app/services/room/room.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from 'src/app/services/Auth/auth.service';

@Component({
  selector: 'app-room-booking-edit',
  templateUrl: './room-booking.component.html',
  styleUrls: ['./room-booking.component.css']
})
export class RoomBookingAddComponent  {
  selectedRoomId: string | null = null;
  selectedDate: Date | null = null;
  timeSlotList: RoomDateTimeSlot[] = [];

  constructor(private bookingService: BookingService, private authService: AuthService, private roomService: RoomService, private snackBar: MatSnackBar) {}

  onRoomSelected(roomId: any) {
    this.selectedRoomId = roomId;
    this.selectedDate = null;
    this.timeSlotList = [];
  }

  onDateSelected(date: Date) {
    this.selectedDate = date;
    this.loadTimeSlots()
  }  

  isAnyTimeSlotSelected(): boolean {
    return this.timeSlotList.some(slot => slot.selecionado);
  }


  saveSelection() {
    const itensSelecionados = this.timeSlotList.filter(item => item.selecionado).map(x => x.id);

    const novaReserva: RequestBooking = {
      id: itensSelecionados,
      roomId: this.selectedRoomId!,
      userId: this.authService.getUserIdFromToken()
    };

    this.bookingService.saveSelectedTimeSlots(novaReserva).subscribe({
      next: (response) => {
        this.snackBar.open('Reserva efetuada com sucesso!', 'Fechar', {
          duration: 3000,
        });
        this.loadTimeSlots()
      },
      error: (err) => {
        this.snackBar.open('Erro ao efetuar a reserva. Tente novamente.', 'Fechar', {
          duration: 3000,
        });
        this.loadTimeSlots()
      },
    });    
    
  }

  loadTimeSlots() {
    if (this.selectedRoomId && this.selectedDate)
      {
        const formattedDate = new Intl.DateTimeFormat('en-CA').format(this.selectedDate);
        this.roomService.getRoomDateTimeSlots(this.selectedRoomId, formattedDate).subscribe(timeSlots => {
          this.timeSlotList = timeSlots;
        });
      }
  }


}
