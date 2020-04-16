import React, { useContext } from 'react';
import { Form as FinalForm, Field } from 'react-final-form';
import { Form, Button, Label, Header } from 'semantic-ui-react';
import TextInput from '../../app/common/form/TextInput';
import { RootStoreContext } from '../../app/stores/rootStore';
import { IUserFormValues } from '../../app/models/user';
import { FORM_ERROR } from 'final-form';
import { combineValidators, isRequired } from 'revalidate';
import ErrorMessage from '../../app/common/form/ErrorMessage';

const validate = combineValidators({
    email: isRequired('Email'),
    password: isRequired('Password'),
});

const LoginForm = () => {
    const rootStore = useContext(RootStoreContext);
    const { login } = rootStore.userStore;

    return (
        <FinalForm
            onSubmit={(values: IUserFormValues) =>
                login(values).catch((error) => ({
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
                        content='Login to deploy-O-mat'
                        color='teal'
                        textAlign='center'
                    />
                    <Field
                        name='email'
                        component={TextInput}
                        placeholder='email'
                    />
                    <Field
                        name='password'
                        component={TextInput}
                        placeholder='password'
                        type='password'
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
                        content='Login'
                        loading={submitting}
                        fluid
                    />
                </Form>
            )}
        ></FinalForm>
    );
};

export default LoginForm;
