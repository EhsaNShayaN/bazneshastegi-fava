import {Component} from '@angular/core';
import {FormArray, Validators} from '@angular/forms';
import {BaseFormComponent} from '../base-form-component';
import {CustomConstants} from '../../../core/constants/custom.constants';

@Component({
  selector: 'app-relations',
  templateUrl: './relations.component.html',
  styleUrl: '../forms.scss',
  standalone: false
})
export class RelationsComponent extends BaseFormComponent {
  constructor() {
    super();
    this.getRelations();
  }

  override createForm() {
    this.form = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      relation: ['', Validators.required],
      grade: ['', Validators.required],
      loanAmount: [null, [Validators.required, Validators.min(1000)]],
      requestType: ['', Validators.required], // محصل یا دانشجو
      dependents: this.fb.array([]),
      attachments: this.fb.array(
        this.requestTypeAttachments.map(s =>
          this.fb.group({
            obj: [s],
            type: [s.lookupName],
            file: [null, s.mandantory ? Validators.required : null],
            uploaded: [false]
          })
        )
      ),
    });
  }

  get dependents() {
    return this.form.get('dependents') as FormArray;
  }

  addDependent() {
    this.dependents.push(
      this.fb.group({
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        relation: ['', Validators.required],
        grade: ['', Validators.required],
        loanAmount: [null],
        dateReceived: ['']
      })
    );
  }

  submit() {
    if (this.form.valid) {
      console.log('📌 اطلاعات وابسته ثبت شد:', this.form.value);
      this.toaster.success(CustomConstants.THE_OPERATION_WAS_SUCCESSFUL, '', {});
      this.form.reset();
      this.form.markAsPristine();
      this.form.markAsUntouched();
    } else {
      this.form.markAllAsTouched();
      console.log(this.findInvalidControls(this.form));
    }
  }
}
