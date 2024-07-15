import { Component, OnInit } from '@angular/core';
import { SharedModule } from '../../../../shared/shared.module';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule, FormControl, FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { UserLoginRequest } from '../../../models/requests/users/user-login-request';
import { AuthBaseService } from '../../../services/abstracts/auth-base.service';
import { Router } from '@angular/router';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-login-form',
  standalone: true,
  imports: [SharedModule, CommonModule, FormsModule, ReactiveFormsModule, ToastModule],
  templateUrl: './login-form.component.html',
  styleUrl: './login-form.component.scss',
  providers: [MessageService, Document]
})
export class LoginFormComponent implements OnInit {

  loginForm!: FormGroup;
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
    this.createLoginForm();

    this.activeTab();
  }
  
  createLoginForm() {
    this.loginForm = this.fb.group({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required])
    });
  }

  activeTab(): void {
    let tabRegister = this.document2.getElementById('tab-register') as HTMLLinkElement;
    if (tabRegister) {
      tabRegister.classList.remove('active')
    }
    let tabLogin = this.document2.getElementById('tab-login') as HTMLLinkElement;
    if (tabLogin) {
      tabLogin.classList.add('active')
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
    if (this.loginForm.valid) {
      let loginModel: UserLoginRequest = Object.assign({}, this.loginForm.value)

      this.authService.login(loginModel).subscribe({
        next: (response) => {
          this.router.navigate(['homepage']);
        },
        error: (err) => {
          console.error('Login failed:', err);
          this.messageService.add({ severity: 'error', summary: 'Something went wrong', detail: 'Please check the fields and try again. If the error persists, please contact support.', life: 15000 });
        }
      });

      this.loginForm.patchValue({
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
    this.submitted = false;
  }
}