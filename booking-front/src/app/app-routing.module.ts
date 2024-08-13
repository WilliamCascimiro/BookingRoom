import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RoomBookingAddComponent } from './components/room-booking/room-boking-add/room-booking.component';
import { LoginComponent } from './components/login/login.component';
import { RoomBookingListComponent } from './components/room-booking/room-booking-list/room-booking-list.component';
import { RoomBookingEditComponent } from './components/room-booking/room-booking-edit/room-booking-edit.component';
import { RoomDetailComponent } from './components/room-booking/room-detail/room-detail.component';
import { RoomBookingMainComponent } from './components/room-booking/room-booking-main/room-booking-main.component';
import { AuthGuard } from './guards/AuthGuard ';

const routes: Routes = [
  { path: '', redirectTo: '/index', pathMatch: 'full' }, // Rota padrão (redireciona para /cadastro)
  { path: 'index', component: RoomBookingMainComponent, canActivate: [AuthGuard] },
  { path: 'create-booking', component: RoomBookingAddComponent, canActivate: [AuthGuard] },
  { path: 'list-bookings', component: RoomBookingListComponent, canActivate: [AuthGuard] },
  { path: 'edit-booking/:id', component: RoomBookingEditComponent, canActivate: [AuthGuard] },
  { path: 'detail-booking/:id', component: RoomDetailComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
