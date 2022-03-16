import { CapacitorConfig } from '@capacitor/cli';

const config: CapacitorConfig = {
  appId: 'com.CarReservation.ZAP',
  appName: 'ZapFrontend',
  webDir: 'www',
  bundledWebRuntime: false,
  android: {
    allowMixedContent: true
  }
};

export default config;
