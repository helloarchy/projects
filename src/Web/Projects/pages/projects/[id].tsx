import { Text } from '@nextui-org/react'
import { useRouter } from 'next/router'

const ProjectPage = () => {
  const router = useRouter()
  const { id } = router.query

  return (
    <>
      <Text>This is the project page for project {id}</Text>
    </>
  )
}

export default ProjectPage
