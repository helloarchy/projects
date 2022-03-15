import Link from 'next/link'
import Layout from '../components/Layout'

const ProjectsPage = () => (
    <Layout title="Projects | All Projects">
        <h1>Projects</h1>
        <p>This is the page for all projects</p>
        <p>
            <Link href="/">
                <a>Go home</a>
            </Link>
        </p>
    </Layout>
)

export default ProjectsPage
