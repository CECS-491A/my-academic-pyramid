export default {
    state: {
        token: null,
        userId: null,
        category: null,
        schoolId: null
    },
    updateSession(newToken) {
        sessionStorage.SITtoken = newToken
        this.state.token = newToken
    },
    logout() {
        this.state.token = null
        this.state.userId = null
        this.state.category = null
        this.state.schoolId = null
        sessionStorage.removeItem("SITtoken")
        sessionStorage.removeItem("SITuserId")
        sessionStorage.removeItem("SITcategory")
        sessionStorage.removeItem("SITschoolId")
    },
    setUserId(userId) {
        sessionStorage.SITuserId = userId
        this.state.userId = userId
    },
    setCategory(category) {
        sessionStorage.SITcategory = category
        this.state.category = category
    },
    setSchoolId(schoolId){
        sessionStorage.SITschoolId = schoolId
        this.state.schoolId = schoolId
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
        if (sessionStorage.SITcategory != undefined) {
            this.state.category = sessionStorage.SITcategory
        }
    }
}



