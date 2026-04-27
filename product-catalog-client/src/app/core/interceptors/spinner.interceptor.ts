import { HttpInterceptorFn } from "@angular/common/http";
import { inject } from "@angular/core";
import { finalize } from "rxjs";
import { NgxSpinnerService } from "ngx-spinner";

export const spinnerInterceptor: HttpInterceptorFn = (req, next) => {
  const spinnerService = inject(NgxSpinnerService);
  
  spinnerService.show();

  return next(req).pipe(
    finalize(() => spinnerService.hide())
  );
}