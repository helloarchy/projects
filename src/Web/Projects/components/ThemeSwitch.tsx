import { useTheme as useNextTheme } from 'next-themes'
import { Switch, useTheme } from '@nextui-org/react'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faMoon, faSun } from '@fortawesome/free-solid-svg-icons'

const ThemeSwitch = () => {
  const { setTheme } = useNextTheme()
  const { isDark, type } = useTheme()

  return (
    <div>
      The current theme is: {type}
      <Switch
        checked={isDark}
        onChange={(e) => setTheme(e.target.checked ? 'dark' : 'light')}
        iconOn={<FontAwesomeIcon icon={faSun} />}
        iconOff={<FontAwesomeIcon icon={faMoon} />}
      />
    </div>
  )
}

export default ThemeSwitch