import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent {
  @Input() selectedDate: Date | null = null;
  @Output() dateChanged = new EventEmitter<Date>();

  onDateChange() {
    if (this.selectedDate) {
      this.dateChanged.emit(this.selectedDate);
    }
  }

}
