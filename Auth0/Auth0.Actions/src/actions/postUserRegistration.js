/**
* Handler that will be called during the execution of a PostUserRegistration flow.
*
* @param {Event} event - Details about the context and user that has registered.
*/
const axios = require("axios");

exports.onExecutePostUserRegistration = async (event) => {

    // apiUrl, subscriptionKey
    // Secrets can't be null, use false to skip webhook calls
    if (event.secrets.apiUrl == "false") {
        return;
    }

    await axios.post(`${event.secrets.apiUrl}/Auth0/PostRegistration`,
        PostUserRegistration(event.user), {
        headers: {
            "Ocp-Apim-Subscription-Key": event.secrets.subscriptionKey,
            "Content-Type": "application/json"
        }
    }
    );
};

const PostUserRegistration = (user) => ({
    IcsUserId: user.app_metadata["IcsUserId"],
    ProviderIdentity: user.user_id
});
