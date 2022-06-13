import Link from 'next/link'
import { GetServerSideProps, InferGetServerSidePropsType } from 'next'

import Layout from '../components/Layout'
import { Button, Text } from '@nextui-org/react'

type Project = {
  title: string
}

type Data = {
  projects: Project[]
}

export const getServerSideProps: GetServerSideProps = async (context) => {
  // Fetch data from external API
  let data: Data = {
    projects: [
      {
        title: '',
      },
    ],
  }

  try {
    const endpoint = `${process.env.GATEWAY}/api/project`
    console.log(`Sending request to: ${endpoint}`)

    const res = await fetch(endpoint)
    data = await res.json()

    console.log('Fetched data...')
    console.dir(data, { depth: null })
  } catch (e) {
    console.log(e)
  }

  // Pass data to the page via props
  return { props: { data } }
}

function renderProjects(projects: Project[] | undefined) {
  {
    if (projects?.length && projects?.length > 0) {
      return (
        <div>
          <Text h2>Projects list...</Text>
          <ul>
            {projects.map((project: Project) => (
              <li key={project.title}>{project.title}</li>
            ))}
          </ul>
          <Text>End of projects list.</Text>
          <Button>Next UI test</Button>
        </div>
      )
    } else {
      return (
        <div>
          <p>No projects</p>
          <Button>Next UI test</Button>
        </div>
      )
    }
  }
}

const ProjectsPage = ({
  data,
}: InferGetServerSidePropsType<typeof getServerSideProps>) => (
  <Layout title="Projects | All Projects">
    <h1>Projects</h1>
    <p>This is the page for all projects</p>
    {renderProjects(data?.projects)}
    <p>
      <Link href="/">
        <a>Go home</a>
      </Link>
    </p>
  </Layout>
)

export default ProjectsPage
