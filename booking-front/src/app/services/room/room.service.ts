import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Room } from 'src/app/interfaces/Room';
import { RoomDateTimeSlot } from 'src/app/interfaces/RoomTimeSlot';

@Injectable({
  providedIn: 'root'
})
export class RoomService {
  private apiUrl = 'http://localhost:5000';

  constructor(private http: HttpClient) { }

  getRooms(): Observable<Room[]> {
    return this.http.get<Room[]>(`${this.apiUrl}/room`);
  }

  getRoomDateTimeSlots(roomId: string, date?: string) : Observable<RoomDateTimeSlot[]> {
    const url = `${this.apiUrl}/room/${roomId}/get-time-slots?date=${date}`;
    return this.http.get<RoomDateTimeSlot[]>(url); 
  }

  getRoomDateTimeSlotsSelected(roomId: string, date?: string, bookingId?: string) : Observable<RoomDateTimeSlot[]> {
    const url = `${this.apiUrl}/room/${roomId}/get-time-slots-selected?date=${date}&bookingId=${bookingId}`;
    return this.http.get<RoomDateTimeSlot[]>(url);
  }
  
}
