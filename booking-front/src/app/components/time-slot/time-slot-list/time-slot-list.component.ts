import { Component, Input } from '@angular/core';
import { RoomDateTimeSlot } from 'src/app/interfaces/RoomTimeSlot';

@Component({
  selector: 'app-time-slot-list',
  templateUrl: './time-slot-list.component.html',
  styleUrls: ['./time-slot-list.component.css']
})
export class TimeSlotListComponent {
  @Input() timeSlotList: RoomDateTimeSlot[] = [];

  // Lógica para verificar se um checkbox pode ser selecionado
  canBeSelected(index: number): boolean {
    const selectedIndices = this.timeSlotList
      .map((slot, i) => slot.selecionado ? i : -1)
      .filter(i => i !== -1);

    if (selectedIndices.length === 0) {
      return true; // Se nenhum estiver selecionado, todos podem ser selecionados
    }

    const firstSelectedIndex = selectedIndices[0];
    const lastSelectedIndex = selectedIndices[selectedIndices.length - 1];

    // Permitir seleção apenas dos slots consecutivos ou já selecionados
    return index === firstSelectedIndex - 1 || index === lastSelectedIndex + 1 || this.timeSlotList[index].selecionado;
  }

  // Lógica a ser executada quando um time slot é selecionado ou desmarcado
  onSelectionChange(index: number) {
    const selectedIndices = this.timeSlotList
      .map((slot, i) => slot.selecionado ? i : -1)
      .filter(i => i !== -1);

    if (selectedIndices.length > 0) {
      const firstSelectedIndex = selectedIndices[0];
      const lastSelectedIndex = selectedIndices[selectedIndices.length - 1];

      this.timeSlotList.forEach((slot, i) => {
        if (i >= firstSelectedIndex && i <= lastSelectedIndex) {
          slot.selecionado = true; // Mantém a seleção se estiver dentro da sequência
        } else {
          slot.selecionado = false; // Desmarca se estiver fora da sequência
        }
      });
    }
  }
}
