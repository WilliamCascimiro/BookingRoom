import { Component, OnInit } from '@angular/core';
import { ResponseResult } from 'src/app/interfaces/Request_Response/ResponseResult';
import { ListBookingResponse } from 'src/app/interfaces/booking/ListBookingResponse';
import { AuthService } from 'src/app/services/Auth/auth.service';
import { BookingService } from 'src/app/services/booking/booking.service';

@Component({
  selector: 'app-room-booking-list',
  templateUrl: './room-booking-list.component.html',
  styleUrls: ['./room-booking-list.component.css']
})
export class RoomBookingListComponent implements OnInit {
  listBookings: ListBookingResponse[] = []

  constructor(private bookingService: BookingService, private authService: AuthService) {}

  ngOnInit() {
    this.loadListBookings();
  }

  loadListBookings() {
    const userId = this.authService.getUserIdFromToken();
    this.bookingService.getBookings(userId).subscribe(bookings => {
      this.listBookings = bookings.value as ListBookingResponse[];
    });
  }

  deleteBooking(bookingId: string) {
    this.bookingService.deleteBooking(bookingId).subscribe(bookings => {
      this.loadListBookings();
    });
  }
  

}