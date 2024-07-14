import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { LocalStorageBaseService } from '../../features/services/abstracts/local-storage-base.service';

export const AuthInterceptor: HttpInterceptorFn = (req, next) => {

  const localStorageService = inject(LocalStorageBaseService);
  
  const token = localStorageService.get('token');

  const authRequest = req.clone({
    setHeaders: {
      Authorization: `Bearer ${token}`
    }
  });

  return next(authRequest);
};
