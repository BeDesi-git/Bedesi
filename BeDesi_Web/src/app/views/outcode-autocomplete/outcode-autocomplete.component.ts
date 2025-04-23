import { Component, OnInit, EventEmitter, Input, Output, OnChanges, SimpleChanges } from '@angular/core';

import { CommonModule } from '@angular/common';
import { FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
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
  selector: 'outcode-autocomplete',
  templateUrl: 'outcode-autocomplete.component.html',
  styleUrls: ['outcode-autocomplete.component.css'],
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
export class OutcodeAutocomplete implements OnInit {
  
  myControl = new FormControl('', Validators.required);
  options: string[] = [];
  filteredOptions?: Observable<string[]>;
  
  @Input() value: string = '';
  @Output() optionSelected = new EventEmitter<string>();
  constructor(private locationService: LocationService) {

  }

  ngOnInit() {
    this.populateAllOutcodes();

    this.filteredOptions = this.myControl.valueChanges.pipe(
      startWith(''),
      map(value => {
        this.emitValue(value);
        return value ? this._filter(value) : this.options.slice();
      }),
    );
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['value'] && changes['value'].currentValue !== undefined) {
      this.myControl.setValue(this.value);  // Set the value from input property
    }
  }

  displayFn(value: string): string {
    return value;
  }

  private _filter(name: string | null): string[] {
    const filterValue = name?.toLowerCase();
    return this.options.filter(option => option.toLowerCase().startsWith(filterValue ? filterValue : '')).slice(0,5);
  }

  private populateAllOutcodes() {
    this.locationService.getAllOutcodes().subscribe({
      next: (areas) => {
        this.options = areas.result;
      },
      error: (e) => {
        console.error('Error fetching outcode suggestions', e);
      }
    })
  }

  onOptionSelected(event: any) {
    const selectedOption = event.option.value;
    this.emitValue(selectedOption);
  }

  private emitValue(selectedValue:any) {
    this.optionSelected.emit(selectedValue);
  }
}
