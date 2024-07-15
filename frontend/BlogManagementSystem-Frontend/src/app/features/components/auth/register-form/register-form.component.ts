import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule, FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { SharedModule } from '../../../../shared/shared.module';
import { UserRegisterRequest } from '../../../models/requests/users/user-register-request';
import { AuthBaseService } from '../../../services/abstracts/auth-base.service';
import { Router } from '@angular/router';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-register-form',
  standalone: true,
  imports: [SharedModule, CommonModule, FormsModule, ReactiveFormsModule, ToastModule],
  templateUrl: './register-form.component.html',
  styleUrl: './register-form.component.scss',
  providers: [MessageService, Document]
})
export class RegisterFormComponent implements OnInit {

  registerForm!: FormGroup;
  showPassword: boolean = false;
  submitted: boolean = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthBaseService,
    private document2: Document,
    private router: Router,
    private messageService: MessageService
  ) { }

  ngOnInit(): void {
    this.createRegisterForm();

    this.activeTab();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      userName: new FormControl('', [Validators.required, Validators.minLength(3)]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.pattern(/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-])[a-zA-Z0-9#?!@$%^&*-ÇĞİÖŞÜçğıöşü]{8,}$/)])
    });
  }

  activeTab(): void {
    let tabLogin = this.document2.getElementById('tab-login') as HTMLLinkElement;
    if (tabLogin) {
      tabLogin.classList.remove('active')
    }
    let tabRegister = this.document2.getElementById('tab-register') as HTMLLinkElement;
    if (tabRegister) {
      tabRegister.classList.add('active')
    }
  }

  handleInputFocus($event: FocusEvent): void {
    const input = $event.target as HTMLInputElement;
    const label = input.previousElementSibling as HTMLLabelElement;

    if (input.value === '') {
      label.classList.remove('highlight');
    } else {
      label.classList.add('highlight');
    }
  }

  handleInputBlur($event: FocusEvent): void {
    const input = $event.target as HTMLInputElement;
    const label = input.previousElementSibling as HTMLLabelElement;

    if (input.value === '') {
      label.classList.remove('highlight');
    }
  }

  handleInputChange($event: Event): void {
    const input = $event.target as HTMLInputElement;
    const label = input.previousElementSibling as HTMLLabelElement;

    if (input.value === '') {
      label.classList.remove('active', 'highlight');
    } else {
      label.classList.add('active', 'highlight');
    }
  }

  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }

  onSubmit(): void {
    this.submitted = true;
    if (this.registerForm.valid) {
      let registerModel: UserRegisterRequest = Object.assign({}, this.registerForm.value);

      this.authService.register(registerModel).subscribe({
        next: (response) => {
          this.router.navigate(['auth/login']);
        },
        error: (err) => {
          console.error('Register failed:', err);
          this.messageService.add({ severity: 'error', summary: 'Something went wrong', detail: 'Please check the fields and try again. If the error persists, please contact support.', life: 15000 });
        }
      });

      this.registerForm.patchValue({
        userName: '',
        email: '',
        password: ''
      });

      const labels = document.querySelectorAll('label');
      labels.forEach(label => {
        label?.classList.remove('active', 'highlight');
      });
    } else {
      this.messageService.add({ severity: 'warn', summary: 'Missing or incorrect field(s)', detail: 'Fill in the fields with valid data and try again.', life: 10000 });
    }
    if (this.registerForm.get('userName')?.invalid) {
      this.messageService.add({ severity: 'warn', summary: 'Username field', detail: 'Enter a username of at least 3 letters.', life: 10000 });
    }
    if (this.registerForm.get('email')?.invalid) {
      this.messageService.add({ severity: 'warn', summary: 'Email field', detail: 'Enter a valid email address.', life: 10000 });
    }
    if (this.registerForm.get('password')?.invalid) {
      this.messageService.add({ severity: 'warn', summary: 'Password field', detail: 'It must be at least 8 characters long and contain uppercase letters, lowercase letters, numbers and special characters.', life: 10000 });
    }
    this.submitted = false;
  }
}