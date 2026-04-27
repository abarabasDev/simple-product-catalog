import { Component, Input } from '@angular/core';
import { AbstractControl } from '@angular/forms';

@Component({
  selector: 'form-validation-message',
  templateUrl: './form-validation-message.component.html',
  styleUrls: ['./form-validation-message.component.css']
})
export class FormValidationMessageComponent {
  @Input() control!: AbstractControl
}
