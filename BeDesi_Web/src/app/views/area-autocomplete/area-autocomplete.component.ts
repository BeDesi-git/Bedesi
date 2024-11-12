import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { AsyncPipe } from '@angular/common';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { LocationService } from '../../services/location.service';

//export interface User {
//  name: string;
//}

/**
 * @title Display value autocomplete
 */
@Component({
  selector: 'area-autocomplete',
  templateUrl: 'area-autocomplete.component.html',
  styleUrls: ['area-autocomplete.component.css'],
  standalone: true,
  imports: [
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatAutocompleteModule,
    ReactiveFormsModule,
    AsyncPipe,
    CommonModule
  ],
})
export class AreaAutocomplete implements OnInit {
  myControl = new FormControl<string>('');
  options: string[] = [];
  filteredOptions?: Observable<string[]>;

  @Output() optionSelected = new EventEmitter<string>();
  constructor(private locationService: LocationService) {

  }

  ngOnInit() {
    this.populateAllAreas();

    this.filteredOptions = this.myControl.valueChanges.pipe(
      startWith(''),
      map(value => {
        return value ? this._filter(value) : this.options.slice();
      }),
    );
  }

  displayFn(value: string): string {
    return value;
  }

  private _filter(name: string | null): string[] {
    const filterValue = name?.toLowerCase();
    return this.options.filter(option => option.toLowerCase().startsWith(filterValue ? filterValue : '')).slice(0,5);
  }

  private populateAllAreas() {
    this.locationService.getAllAreas().subscribe({
      next: (areas) => {
        this.options = areas.result;
      },
      error: (e) => {
        console.error('Error fetching area suggestions', e);
      }
    })
  }

  onOptionSelected(event: any) {
    const selectedOption = event.option.value;
    this.optionSelected.emit(selectedOption);
  }
}
