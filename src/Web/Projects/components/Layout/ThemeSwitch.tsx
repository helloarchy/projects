import { useTheme as useNextTheme } from 'next-themes'
import { Switch, useTheme } from '@nextui-org/react'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faMoon, faSun } from '@fortawesome/free-solid-svg-icons'

const ThemeSwitch = () => {
  const { setTheme } = useNextTheme()
  const { isDark, type } = useTheme()

  return (
    <Switch
      checked={isDark}
      onChange={(e) => setTheme(e.target.checked ? 'dark' : 'light')}
      iconOn={<FontAwesomeIcon icon={faMoon} />}
      iconOff={<FontAwesomeIcon icon={faSun} />}
    />
  )
}

export default ThemeSwitch
