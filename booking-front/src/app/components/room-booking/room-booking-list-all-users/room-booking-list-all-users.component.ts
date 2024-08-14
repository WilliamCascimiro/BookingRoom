import { Component, OnInit } from '@angular/core';
import { ListBookingResponse } from 'src/app/interfaces/booking/ListBookingResponse';
import { AuthService } from 'src/app/services/Auth/auth.service';
import { BookingService } from 'src/app/services/booking/booking.service';

@Component({
  selector: 'app-room-booking-list-all-users',
  templateUrl: './room-booking-list-all-users.component.html',
  styleUrls: ['./room-booking-list-all-users.component.css']
})
export class RoomBookingListAllUsersComponent implements OnInit {
  listBookings: ListBookingResponse[] = []

  constructor(private bookingService: BookingService, private authService: AuthService) {}

  ngOnInit() {
    this.loadListBookings();
  }

  loadListBookings() {
    this.bookingService.getAllBookings().subscribe(bookings => {
      this.listBookings = bookings.value as ListBookingResponse[];
    });
  }

  deleteBooking(bookingId: string) {
    this.bookingService.deleteBooking(bookingId).subscribe(bookings => {
      this.loadListBookings();
    });
  }
}
