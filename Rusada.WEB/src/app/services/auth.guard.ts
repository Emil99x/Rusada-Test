import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const token = sessionStorage.getItem('loggedInUser');
  if (token) {
    // TODO : find  a way to validate google jwt token
    return true;
  } else {
    router.navigate(['login']);
  }
  return true;
};
