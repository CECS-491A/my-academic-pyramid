export default {
    state: {
        token: null,
        userId: null
    },
    updateSession(newToken) {
        sessionStorage.SITtoken = newToken
        this.state.token = newToken
    },
    logout() {
        this.state.token = null
        sessionStorage.removeItem("SITtoken")
        sessionStorage.removeItem("SITuserId")
    },
    setUserId(userId) {
        console.log("in setUserId")
        console.log(userId)
        sessionStorage.SITuserId = userId
        this.state.userId = userId
    },
    synchAppSession() {
        // run this so that the state will be updated
        // with SITtoken and userId when a refresh occurs.
        if (sessionStorage.SITtoken != undefined) {
            this.state.token = sessionStorage.SITtoken
        }
        if (sessionStorage.SITuserId != undefined) {
            this.state.userId = sessionStorage.SITuserId
        }
    }
}



