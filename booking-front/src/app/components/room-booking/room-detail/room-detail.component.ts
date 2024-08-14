import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { Room } from 'src/app/interfaces/Room';
import { RoomDateTimeSlot } from 'src/app/interfaces/RoomTimeSlot';
import { DetailBookingResponse } from 'src/app/interfaces/booking/DetailBookingResponse';
import { AuthService } from 'src/app/services/Auth/auth.service';
import { BookingService } from 'src/app/services/booking/booking.service';
import { RoomService } from 'src/app/services/room/room.service';

@Component({
  selector: 'app-room-detail',
  templateUrl: './room-detail.component.html',
  styleUrls: ['./room-detail.component.css']
})
export class RoomDetailComponent {
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

  isUserAdmin(){
    return this.authService.isUserAdmin();
  }

  isUser(){
    return this.authService.isUser();
  }

}


