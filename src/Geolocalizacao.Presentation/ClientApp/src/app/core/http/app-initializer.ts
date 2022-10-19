import { AuthApiService } from "src/app/features/auth/api/auth.api";

export function appInitializer(authService: AuthApiService) {
  return () =>
    new Promise((resolve) => {
      console.log('refresh token on app start up')
      //authService.refreshToken().subscribe().add(resolve);
    });
}