import React from 'react';
import { Container, Table } from 'semantic-ui-react';

const BuildStatus: React.FC = () => {
    return (
        <Container>
            <Table celled>
                <Table.Header>
                    <Table.Row>
                        <Table.HeaderCell>Repository</Table.HeaderCell>
                        <Table.HeaderCell>Status</Table.HeaderCell>
                        <Table.HeaderCell>Action</Table.HeaderCell>
                        <Table.HeaderCell>Branch</Table.HeaderCell>
                    </Table.Row>
                </Table.Header>

                <Table.Body>
                    <Table.Row key={0}>
                        <Table.Cell>deploy-O-mat</Table.Cell>
                        <Table.Cell>
                            <img
                                src={
                                    'https://github.com/Marcel-B/Deploy-O-Mat/workflows/Publish%20Docker/badge.svg'
                                }
                                alt={'build status'}
                                style={{ height: '20px' }}
                            />
                        </Table.Cell>
                        <Table.Cell>Docker-Hub publish</Table.Cell>
                        <Table.Cell>Master</Table.Cell>
                    </Table.Row>

                    <Table.Row key={1}>
                        <Table.Cell>deploy-O-mat</Table.Cell>
                        <Table.Cell>
                            <img
                                src={
                                    'https://github.com/Marcel-B/Deploy-O-Mat/workflows/.NET%20Core/badge.svg'
                                }
                                alt={'build status'}
                                style={{ height: '20px' }}
                            />
                        </Table.Cell>
                        <Table.Cell>.NET core build</Table.Cell>
                        <Table.Cell>Master</Table.Cell>
                    </Table.Row>

                    <Table.Row key={2}>
                        <Table.Cell>deploy-O-mat</Table.Cell>
                        <Table.Cell>
                            <img
                                src={
                                    'https://github.com/Marcel-B/Deploy-O-Mat/workflows/Docker%20Image%20CI/badge.svg'
                                }
                                alt={'build status'}
                                style={{ height: '20px' }}
                            />
                        </Table.Cell>
                        <Table.Cell>Docker Image build</Table.Cell>
                        <Table.Cell>Master</Table.Cell>
                    </Table.Row>

                    <Table.Row key={3}>
                        <Table.Cell>b-velop.Utilities</Table.Cell>
                        <Table.Cell>
                            <img
                                src={
                                    'https://github.com/Marcel-B/Utilities/workflows/.NET%20Core/badge.svg'
                                }
                                alt={'build status'}
                                style={{ height: '20px' }}
                            />
                        </Table.Cell>
                        <Table.Cell>.NET standard build</Table.Cell>
                        <Table.Cell>Master</Table.Cell>
                    </Table.Row>
                    <Table.Row key={4}>
                        <Table.Cell>Slipways.Web</Table.Cell>
                        <Table.Cell>
                            <img
                                src={
                                    'https://github.com/Marcel-B/Slipways.Web/workflows/ASP.NET%20Core%20CI/badge.svg?branch=master'
                                }
                                alt={'build status'}
                                style={{ height: '20px' }}
                            />
                        </Table.Cell>
                        <Table.Cell>.NET core build</Table.Cell>
                        <Table.Cell>Master</Table.Cell>
                    </Table.Row>
                </Table.Body>

                {/* <Table.Footer>
                 <Table.Row>
                     <Table.HeaderCell colSpan='3'>
                         <Menu floated='right' pagination>
                             <Menu.Item as='a' icon>
                             <Menu.Item as='a' icon>
                                 <Icon name='chevron left' />
                            </Menu.Item>
                             <Menu.Item as='a'>1</Menu.Item>
                             <Menu.Item as='a'>2</Menu.Item>
                             <Menu.Item as='a'>3</Menu.Item>
                         <Menu.Item as='a'>4</Menu.Item>
                             <Menu.Item as='a' icon>
                                 <Icon name='chevron right' />
                             </Menu.Item>
                         </Menu>
                     </Table.HeaderCell>
                 </Table.Row>
             </Table.Footer> */}
            </Table>
        </Container>
    );
};

export default BuildStatus;
