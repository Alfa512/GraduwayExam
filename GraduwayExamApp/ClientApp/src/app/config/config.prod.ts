import { Injectable } from '@angular/core';

@Injectable()

export class AppConfig {



  private _config: { [key: string]: string };

  constructor() {

    this._config = {

      PathAPI: 'https://test-gw-app-api.azurewebsites.net/api/'

    };

  }

  get setting(): { [key: string]: string } {

    return this._config;

  }

  get(key: any) {

    return this._config[key];

  }

};
