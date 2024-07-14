import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthBaseService } from '../../features/services/abstracts/auth-base.service';

export const PreventLoginAccessGuard: CanActivateFn = (route, state) => {

  const authService = inject(AuthBaseService);
  const router = inject(Router);

  if(authService.isAuthenticated()){
    router.navigate([""])
    return false;

  } else {
    return true;
    
  }
};
