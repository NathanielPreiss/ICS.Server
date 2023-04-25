//const axios = require("axios");

///**
// * Handler that will be called during the execution of a PostChangePassword flow.
// *
// * @param {Event} event - Details about the user and the context in which the change password is happening.
// */

//const postChangePassword = async (event) => {
//  await axios.post(`${event.secrets.apiUrl}/password-changed`,
//    passwordChanged(event.user), {
//        headers: {
//          "Ocp-Apim-Subscription-Key": event.secrets.subscriptionKey,
//          "Content-Type": "application/json"
//        }
//      }
//  );
//};

//const passwordChanged = (user) => {
//    UserId = user.user_id
//}

//module.exports = postChangePassword;