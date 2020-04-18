import React, { useContext } from 'react';
import { Form as FinalForm, Field } from 'react-final-form';
import { IDockerImage } from '../../app/models/dockerImage';
import { FORM_ERROR } from 'final-form';
import { combineValidators, isRequired } from 'revalidate';
import { Header, Form, Button } from 'semantic-ui-react';
import TextInput from '../../app/common/form/TextInput';
import ErrorMessage from '../../app/common/form/ErrorMessage';
import { RootStoreContext } from '../../app/stores/rootStore';

const validate = combineValidators({
    name: isRequired('Name'),
    repoName: isRequired('Repo Name'),
});

const UpdateImageForm: React.FC<{ dockerImage: IDockerImage }> = ({
    dockerImage,
}) => {
    const rootStore = useContext(RootStoreContext);
    const { updateImage } = rootStore.dockerImageStore;

    return (
        <FinalForm
            onSubmit={(dockerImg: IDockerImage) =>
                updateImage(dockerImg).catch((error) => ({
                    [FORM_ERROR]: error,
                }))
            }
            validate={validate}
            render={({
                handleSubmit,
                submitting,
                form,
                submitError,
                invalid,
                pristine,
                dirtySinceLastSubmit,
            }) => (
                <Form onSubmit={handleSubmit} error>
                    <Header
                        as='h2'
                        content='Update Docker Image'
                        color='teal'
                        textAlign='center'
                    />
                    <Field
                        name='name'
                        component={TextInput}
                        placeholder='name'
                        value={dockerImage.name}
                    />
                    <Field
                        name='repoName'
                        component={TextInput}
                        placeholder='Repo Name'
                        value={dockerImage.repoName}
                    />
                    {submitError && !dirtySinceLastSubmit && (
                        <ErrorMessage
                            error={submitError}
                            text='Invalid email or password'
                        />
                    )}
                    <Button
                        color='teal'
                        disabled={
                            (invalid && !dirtySinceLastSubmit) || pristine
                        }
                        content='Update'
                        loading={submitting}
                        fluid
                    />
                </Form>
            )}
        ></FinalForm>
    );
};

export default UpdateImageForm;
