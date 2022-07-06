import Link from 'next/link'
import Layout from '../components/Layout/Layout'
import Account from '../components/Account'

const AboutPage = () => (
  <Layout title="About | Archy.dev">
    <h1>About</h1>
    <p>This is the about page</p>
    <p>
      <Link href="/">
        <a>Go home</a>
      </Link>
    </p>
    <Account />
  </Layout>
)

export default AboutPage
