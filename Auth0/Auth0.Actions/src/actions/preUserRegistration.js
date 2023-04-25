/**
* Handler that will be called during the execution of a PreUserRegistration flow.
*
* @param {Event} event - Details about the context and user that is attempting to register.
* @param {PreUserRegistrationAPI} api - Interface whose methods can be used to change the behavior of the signup.
*/
const axios = require("axios");

exports.onExecutePreUserRegistration = async (event, api) => {

    // apiUrl, subscriptionKey
    // Secrets can't be null, use false to skip webhook calls
    if (event.secrets.apiUrl == "false") {
        return;
    }

    await axios.post(`${event.secrets.apiUrl}/Auth0/PreRegistration`,
        PreUserRegistration(event.user), {
        headers: {
            "Ocp-Apim-Subscription-Key": event.secrets.subscriptionKey,
            "Content-Type": "application/json"
        }
    }
    ).then(function (response) {
        if (response.status == 200) {
            api.user.setAppMetadata("IcsUserId", response.data.icsUserId)
        }
        else {
            api.access.deny("Internal reason", "Failed to register.")
        }
    });
};

const PreUserRegistration = (user) => ({
    Email: user.email,
});
