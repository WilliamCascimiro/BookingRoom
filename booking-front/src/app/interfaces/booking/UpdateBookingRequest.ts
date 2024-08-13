export interface UpdateBookingRequest {
    bookingId: string;
    timeSlotsId: string[];
    roomId: string;
    userId: string | null;
}
