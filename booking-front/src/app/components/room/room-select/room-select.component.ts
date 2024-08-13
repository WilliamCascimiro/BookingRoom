import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Room } from 'src/app/interfaces/Room';
import { RoomService } from 'src/app/services/room/room.service';

@Component({
  selector: 'app-room-select',
  templateUrl: './room-select.component.html',
  styleUrls: ['./room-select.component.css']
})
export class RoomSelectComponent implements OnInit  {
  rooms: Room[] = [];
  @Output() roomSelected = new EventEmitter<string>();
  
  constructor(private roomService: RoomService) {}

  ngOnInit() {
    this.roomService.getRooms().subscribe(rooms => {
      this.rooms = rooms;
    });
  }

  onRoomChange(selectedRoomId: string) {
    this.roomSelected.emit(selectedRoomId);
  }


}
