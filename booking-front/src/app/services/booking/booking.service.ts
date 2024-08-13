import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ResponseResult } from 'src/app/interfaces/Request_Response/ResponseResult';
import { DetailBookingResponse } from 'src/app/interfaces/booking/DetailBookingResponse';
import { UpdateBookingRequest } from 'src/app/interfaces/booking/UpdateBookingRequest';
import { RequestBooking } from 'src/app/interfaces/Request';
 

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  private apiUrl = 'http://localhost:5000/booking';

  constructor(private http: HttpClient) { }
  getCliente(id: number) {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<any>(url);
  }

  cadastrarCliente(cliente: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, cliente);
  }

  getBookings(userId: string | null): Observable<ResponseResult> {
    const url = `${this.apiUrl}/ByUser/${userId}`;
    console.log(this.http.get<ResponseResult>(url))
    return this.http.get<ResponseResult>(url);
  }

  getBooking(bookingId: string): Observable<DetailBookingResponse> {
    const url = `${this.apiUrl}/ByBooking/${bookingId}`;
    console.log(this.http.get<DetailBookingResponse>(url))
    return this.http.get<DetailBookingResponse>(url);
  }

  deleteBooking(bookingId: string){
    const url = `${this.apiUrl}/${bookingId}`;
    console.log(url);
    return this.http.delete<boolean>(url);
  }

  atualizarCliente(cliente: any) {
    const url = `${this.apiUrl}/${cliente.id}`;
    return this.http.put<any>(url, cliente);
  }

  updateBooking(request: UpdateBookingRequest) {
    const url = `${this.apiUrl}`;
    return this.http.put(url, request);
  }

  saveSelectedTimeSlots(request: RequestBooking) {
    const url = `${this.apiUrl}`;
    return this.http.post(url, request);
  }

}
