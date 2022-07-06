import { createTheme, Theme } from '@nextui-org/react'
import { BaseTheme } from '@nextui-org/react/types/theme/types'

import sharedTheme from './shared'

const theme: BaseTheme = {
  ...sharedTheme, // Extend the common themes
  colors: {
    primaryLight: '#ff0000',
  },
}

const lightTheme: Theme = createTheme({
  type: 'light',
  className: 'light-theme',
  theme,
})

export default lightTheme
