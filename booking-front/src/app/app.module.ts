import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatNativeDateModule } from '@angular/material/core';
import {MatCardModule} from '@angular/material/card';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatListModule} from '@angular/material/list';
import { CalendarComponent } from './components/calendar/calendar.component';
import { TimeSlotListComponent } from './components/time-slot/time-slot-list/time-slot-list.component';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { LoginComponent } from './components/login/login.component';
import { JWT_OPTIONS, JwtHelperService, JwtModule } from '@auth0/angular-jwt';
import { AuthService } from './services/Auth/auth.service';
import { AuthInterceptor } from './services/Auth/auth.interceptor';
import { RoomBookingAddComponent } from './components/room-booking/room-boking-add/room-booking.component';
import { RoomBookingEditComponent } from './components/room-booking/room-booking-edit/room-booking-edit.component';
import { RoomBookingListComponent } from './components/room-booking/room-booking-list/room-booking-list.component';
import {MatIconModule} from '@angular/material/icon';
import { RoomSelectComponent } from './components/room/room-select/room-select.component';
import { RoomDetailComponent } from './components/room-booking/room-detail/room-detail.component';
import { RoomBookingMainComponent } from './components/room-booking/room-booking-main/room-booking-main.component';

export function tokenGetter() { 
  return localStorage.getItem("jwt"); 
}

@NgModule({
  declarations: [
    AppComponent,
    RoomBookingAddComponent,
    RoomSelectComponent,
    CalendarComponent,
    TimeSlotListComponent,
    LoginComponent,
    RoomBookingEditComponent,
    RoomBookingListComponent,
    RoomDetailComponent,
    RoomBookingMainComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatSlideToggleModule,
    ReactiveFormsModule,
    MatFormFieldModule, 
    MatSelectModule, 
    MatInputModule, 
    FormsModule,
    MatCardModule, 
    MatDatepickerModule, 
    MatNativeDateModule,
    MatCheckboxModule,
    MatListModule,
    MatButtonModule,
    MatSnackBarModule,
    MatIconModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:5001", "localhost:4200", "localhost:44349", "localhost:5000"],
        disallowedRoutes: []
      }
    })
  ],
  providers: [
    HttpClientModule,
    AuthService,
    // JwtHelperService,
    // {
    //   provide: HTTP_INTERCEPTORS,
    //   useClass: AuthInterceptor,
    //   multi: true
    // }
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    {
      provide: JWT_OPTIONS,
      useValue: JWT_OPTIONS
    }

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
