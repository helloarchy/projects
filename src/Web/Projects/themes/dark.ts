import { Theme } from '@nextui-org/react'
import sharedTheme from './shared'


const theme: Theme = {
  theme: {
    colors: {},
    space: {},
    fonts: {}
  }
}

const darkTheme: Theme = {
  ...sharedTheme,
  type: "dark",
  theme
}

export default darkTheme;