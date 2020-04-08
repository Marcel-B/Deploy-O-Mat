import React, { useContext, useEffect } from 'react';
import { RouteComponentProps } from 'react-router-dom';
import { observer } from 'mobx-react-lite';
import DockerImageStore from '../../app/stores/dockerImageStore';
import { LoadingComponent } from '../../app/layout/LoadingComponent';

interface IDetailParams {
    id: string;
}
const DockerImageDetails: React.FC<RouteComponentProps<IDetailParams>> = ({
    match,
}) => {
    const dockerImageStore = useContext(DockerImageStore);
    const { dockerImage, loadDockerImage, loadingInitial } = dockerImageStore;

    useEffect(() => {
        loadDockerImage(match.params.id);
    }, [loadDockerImage, match.params.id]);

    if (loadingInitial || !dockerImage)
        return <LoadingComponent content='Loading service...' />;

    return (
        <div>
            <h1>Docker Image Details</h1>
            <p>{dockerImage.name}</p>
        </div>
    );
};

export default observer(DockerImageDetails);
