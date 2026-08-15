import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, FormBuilder, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-contact-us',
  // imports: [FormsModule, CommonModule, ReactiveFormsModule],
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './contact-us.html',
  styleUrl: './contact-us.css',
})
// export class ContactUs {}

export class ContactUs implements OnInit {
  contactForm!: FormGroup;
  userId=0;
  constructor(
    private fb: FormBuilder
  ) {
    this.contactForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      subject: ['', Validators.required],
      message: ['', Validators.required],
    });
  }

  ngOnInit(): void {
  }

  triggerError() {
    // Simulate a non-HTTP error
    throw new Error('This is a simulated error');
  }

  onSubmit() {
    if (this.contactForm.valid) {
    
    }
  }
}