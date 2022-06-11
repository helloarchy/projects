import { createTheme, Theme } from '@nextui-org/react'
import { BaseTheme } from '@nextui-org/react/types/theme/types'

import sharedTheme from './shared'

const theme: BaseTheme = {
  ...sharedTheme, // Extend the common themes
  colors: {
    primaryLight: '#13ff02'
  },
  fonts: {
    sans: "'Times New Roman'"
  }
}

const darkTheme: Theme = createTheme({
  type: "dark",
  className: "dark-theme",
  theme
});

export default darkTheme;