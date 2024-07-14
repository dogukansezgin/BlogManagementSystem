import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthBaseService } from '../../features/services/abstracts/auth-base.service';

export const AuthGuard: CanActivateFn = (route, state) => {

  const authService = inject(AuthBaseService);
  const router = inject(Router);

  if(authService.isAuthenticated()){
    return true;

  } else {
    router.navigate(["auth/login"])
    return false;
    
  }
};
