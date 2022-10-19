export class Alert {
  id = '';
  type = AlertType.Info;
  message = '';
  autoClose = false;
  keepAfterRouteChange = true;
  fade = false;

  constructor(init?: Partial<Alert>) {
      Object.assign(this, init);
  }
}

export enum AlertType {
  Success,
  Error,
  Info,
  Warning
}
