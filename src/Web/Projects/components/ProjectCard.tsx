import { Project } from '../types/project'
import {
  Button,
  Card,
  Col,
  Divider,
  Link,
  Row,
  Spacer,
  Text,
} from '@nextui-org/react'

type Props = {
  project: Project
}

const ProjectCard = ({ project }: Props) => {
  const defaultImageSource = 'https://via.placeholder.com/350x350'
  const defaultImageAlt = 'Placeholder image'

  return (
    <Card>
      <Card.Header>
        <Text h3>{project.title}</Text>
      </Card.Header>
      <Card.Image
        src={project.imageSource ?? defaultImageSource}
        width={'100%'}
        height={340}
        objectFit={'cover'}
        alt={project.imageDescription ?? defaultImageAlt}
      />
      <Divider />
      <Card.Body>
        <Text>Completed? {project.isComplete ? 'Yes' : 'No'}</Text>
        <Text>{project.shortDescription}</Text>
      </Card.Body>
      <Divider />
      <Card.Footer>
        <Col>
          <Row justify={'flex-end'}>
            <Col>
              <Link icon href={''}>
                View source
              </Link>
            </Col>
          </Row>
          <Spacer />
          <Row justify={'flex-end'}>
            <Col>
              <Button>Goto Live</Button>
            </Col>
            <Spacer />
            <Col>
              <Link
                href={`projects/${encodeURI(project.title)}?id=${encodeURI(
                  project.id,
                )}`}
              >
                <Button>More info</Button>
              </Link>
            </Col>
          </Row>
        </Col>
      </Card.Footer>
    </Card>
  )
}

export default ProjectCard
