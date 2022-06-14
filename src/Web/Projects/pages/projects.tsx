import Link from 'next/link'
import { GetServerSideProps, InferGetServerSidePropsType } from 'next'

import Layout from '../components/Layout'
import { Button, Container, Grid, Text } from '@nextui-org/react'
import { Project } from '../types/project'
import ProjectCard from '../components/ProjectCard'

type Data = {
  projects: Project[]
}

export const getServerSideProps: GetServerSideProps = async (context) => {
  let projects: Project[] = []
  let data: Data = {
    projects
  }

  try {
    const endpoint = `${process.env.GATEWAY}/api/project`
    console.log(`Sending request to: ${endpoint}`)

    const res = await fetch(endpoint)
    data.projects = await res.json()

    console.log('Fetched data.')
  } catch (e) {
    console.log(e)
  }

  return {
    props: {
      data
    },
  }
}

function renderProjects(projects: Project[] | undefined) {
  {
    if (projects?.length && projects?.length > 0) {
      return (
        <div>
          <Text h2>Projects list...</Text>
          <Grid.Container gap={2} justify={"center"}>
            {projects.map((project: Project) => (
              <Grid xs={12} sm={4}>
                <ProjectCard project={project} />
              </Grid>
            ))}
          </Grid.Container>
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

const ProjectsPage = (
  { data }: InferGetServerSidePropsType<typeof getServerSideProps>,
) => {
  return (
    <Layout title='Projects | All Projects'>
      <h1>Projects</h1>
      <p>This is the page for all projects</p>
      {renderProjects(data?.projects)}
      <p>
        <Link href='/'>
          <a>Go home</a>
        </Link>
      </p>
    </Layout>
  )
}

export default ProjectsPage
