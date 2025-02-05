import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { TokenBaseService } from '../../features/services/abstracts/token-base.service';

export const RoleGuard: CanActivateFn = (route, state) => {

    const tokenService = inject(TokenBaseService);
    const router = inject(Router);

    const expectedRoles = route.data['expectedRoles'];
    const userRoles = tokenService.getUserRoles();

    let hasRole = false;

    if (!expectedRoles || !userRoles) {
        router.navigate(['']);
        return false;
    }
    
    for (const role of expectedRoles) {
        if (userRoles.includes(role)){
            hasRole = true
        }
    }

    if(hasRole){
        return true;

    } else {
        router.navigate([""])
        return false;
        
    }
};
