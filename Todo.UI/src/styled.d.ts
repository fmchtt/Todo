import "styled-components";

declare module "styled-components" {
  export interface DefaultTheme {
    primaryColor: string;
    colors: {
      900: string;
      800: string;
      700: string;
      600: string;
      500: string;
      400: string;
      300: string;
      200: string;
      100: string;
    };
    font: {
      bold: string;
      medium: string;
      thin: string;
    };
    gradients: {
      full: string;
      semi: string;
    };
  }
}
