import { Theme } from '@nextui-org/react'
import sharedTheme from './shared'

const theme: Theme = {
  theme: {
    colors: {},
    space: {},
    fonts: {

    }
  }
}

const lightTheme: Theme = {
  ...sharedTheme,
  type: "light",
  theme
}

export default lightTheme;