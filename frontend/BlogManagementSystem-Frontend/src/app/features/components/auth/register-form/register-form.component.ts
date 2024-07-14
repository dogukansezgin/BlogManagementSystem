import { CommonModule, DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule, FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { SharedModule } from '../../../../shared/shared.module';
import { UserRegisterRequest } from '../../../models/requests/users/user-register-request';
import { AuthBaseService } from '../../../services/abstracts/auth-base.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-form',
  standalone: true,
  imports: [SharedModule, CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './register-form.component.html',
  styleUrl: './register-form.component.scss'
})
export class RegisterFormComponent implements OnInit {

  registerForm!: FormGroup;
  showPassword: boolean = false;

  constructor(
    private fb: FormBuilder, 
    private authService: AuthBaseService, 
    @Inject(DOCUMENT) private document: Document,
    private router: Router
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
    let tabLogin = this.document.getElementById('tab-login') as HTMLLinkElement;
    if (tabLogin) {
      tabLogin.classList.remove('active')
    }
    let tabRegister = this.document.getElementById('tab-register') as HTMLLinkElement;
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
    if (this.registerForm.valid) {
      let registerModel: UserRegisterRequest = Object.assign({}, this.registerForm.value);

      this.authService.register(registerModel).subscribe({
        next: (response) => {
          this.router.navigate(['auth/login']);
        },
        error: (err) => {
          console.error('Register failed:', err);
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
    }
  }
}