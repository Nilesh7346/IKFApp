import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormArray, FormGroup, Validators } from '@angular/forms';
import { EmployeeService } from '../shared/employee.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {

  //variables
  employeeRecords: FormArray = this.fb.array([]);
  notification = null;

  constructor(private fb: FormBuilder, private service: EmployeeService, private datepipe: DatePipe) { }

  ngOnInit() {

    //get the list of all employees
    this.service.getEmployeeRecords().subscribe(res => {
      if (res == []) {
        this.addEmployeeRecord()
      }
      else {
        //generate form array
        (res as []).forEach((employeeRecord: any) => {
          this.employeeRecords.push(this.fb.group({
            Id: [employeeRecord.id, Validators.required],
            Name: [employeeRecord.name, Validators.required],
            Skills: [employeeRecord.skills, Validators.required],
            Designation: [employeeRecord.designation, Validators.required],
            DOB: [this.datepipe.transform(employeeRecord.dob, 'yyyy-MM-dd'), Validators.required]
          }));
        })
      }
    });

  }

  //intilize temp obj
  addEmployeeRecord() {
    this.employeeRecords.push(this.fb.group({
      Id: [0, Validators.required],
      Name: ['', Validators.required],
      Skills: [0, Validators.min(1)],
      Designation: ['', Validators.required],
      DOB: [null, Validators.required]
    }));
  }

  //insert or update the records
  recordSubmitOrUpdate(fg: FormGroup) {
    fg.value.DOB = new Date(fg.value.DOB);
    fg.value.Skills = parseInt(fg.value.Skills);
    if (fg.value.Id == 0) {
      this.service.postEmployeeRecord(fg.value).subscribe((res:
        any) => {
        fg.patchValue({
          Id: parseInt(res.id),          
        });       
        this.showNotification('insert');
      })
    }
    else {
      this.service.patchEmployeeRecord(fg.value).subscribe((res:
        any) => {     
        this.showNotification('update');
      })

    }

  }

  //delete  records
  deleteRecord(id: number, index) {
    if (id == 0) {
      this.employeeRecords.removeAt(index);
    }
    else if (confirm('Are you sure to delete this record ?')) {
      this.service.deleteEmployeeRecord(id).subscribe((res:
        any) => {
        this.employeeRecords.removeAt(index);
        this.showNotification('delete');
      })
    }
  }

  //shows the notification
  showNotification(category) {
    switch (category) {
      case 'insert':
        this.notification = { class: 'text-success', message: 'saved!' };
        break;
      case 'update':
        this.notification = { class: 'text-primary', message: 'updated!' };
        break;
      case 'delete':
        this.notification = { class: 'text-danger', message: 'deleted!' };
        break;
      default:
        break;
    }

    setTimeout(() => {
      this.notification=null
    },2000);
  }

}
