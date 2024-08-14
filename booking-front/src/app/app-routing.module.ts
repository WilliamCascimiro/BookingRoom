import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RoomBookingAddComponent } from './components/room-booking/room-boking-add/room-booking.component';
import { LoginComponent } from './components/login/login.component';
import { RoomBookingListComponent } from './components/room-booking/room-booking-list/room-booking-list.component';
import { RoomBookingEditComponent } from './components/room-booking/room-booking-edit/room-booking-edit.component';
import { RoomDetailComponent } from './components/room-booking/room-detail/room-detail.component';
import { RoomBookingMainComponent } from './components/room-booking/room-booking-main/room-booking-main.component';
import { AuthGuard } from './guards/AuthGuard ';
import { RoomBookingListAllUsersComponent } from './components/room-booking/room-booking-list-all-users/room-booking-list-all-users.component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'index', component: RoomBookingMainComponent, canActivate: [AuthGuard], data: { expectedRole: ['admin', 'user'] } },
  { path: 'create-booking', component: RoomBookingAddComponent, canActivate: [AuthGuard], data: { expectedRole: ['admin'] } },
  { path: 'list-my-bookings', component: RoomBookingListComponent, canActivate: [AuthGuard], data: { expectedRole: ['admin'] } },
  { path: 'list-all-bookings', component: RoomBookingListAllUsersComponent, canActivate: [AuthGuard], data: { expectedRole: ['user'] } },
  { path: 'edit-booking/:id', component: RoomBookingEditComponent, canActivate: [AuthGuard], data: { expectedRole: ['admin'] } },
  { path: 'detail-booking/:id', component: RoomDetailComponent, canActivate: [AuthGuard], data: { expectedRole: ['admin', 'user'] } },
  { path: 'login', component: LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
