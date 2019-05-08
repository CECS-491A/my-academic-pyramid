<template>
  <v-form>
    <v-container py-0>
      <v-layout row>

        <v-flex
          xs12
          md4
        >
          <v-text-field
            label="Name"
            v-model="userFullName"
            prepend-inner-icon="person"
            readonly/>
        </v-flex>

        <v-flex
          xs12
          md4
        >
          <v-chip color="orange" text-color="white">
            {{data.Ranking}}
            <v-icon right>star</v-icon>
          </v-chip>
        </v-flex>
        
      </v-layout>

      <v-layout row>

        <v-flex
          xs12
          md4
        >
          <v-text-field
            label="School"
            v-model="data.SchoolName"
            prepend-inner-icon="school"
            readonly/>
        </v-flex>
        <v-flex
          xs12
          md4
        >
          <v-text-field
            label="Department"
            v-model="data.DepartmentName"
            prepend-inner-icon="business"
            readonly/>
        </v-flex>

        <v-alert
            :value="errorMessage"
            id="errorMessage"
            type="error"
            transition="scale-transition"
        >
            {{errorMessage}}
        </v-alert>
        
      </v-layout>

      <v-layout row>

        <v-flex
          xs12
          md4
        >
          <v-text-field
            label="Courses"
            v-model="this.courses"
            prepend-inner-icon="view_list"
            readonly/>
        </v-flex>
        
      </v-layout>
    </v-container>
  </v-form>
</template>

<script>
export default {
  name: 'Profile',
    data () {
        return {
            id: 0,
            msg: 'Hey Sir',
            userId: null,
            userFullName: '',
            courses: [],
            data: {
              FirstName: '',
              MiddleName: '',
              LastName: '',
              SchoolName: '',
              DepartmentName: '',
              Ranking: '',
              Courses: ''
            },
            errorMessage: ''
        }
    },
    created() {
        this.id = this.$route.params.id;
    },
    methods: {
        getUserProfile: function(){
            this.errorMessage = "";

            const url = `${this.$hostname}usermanager/profile`;
            this.axios
            .get(url, {
                params:{
                    AccountId: this.id
                },
                headers: { "Content-Type": "application/json", Authorization: "Bearer " + sessionStorage.SITtoken }
                
            })
            .then(response =>{
                this.data = response.data;
                this.userFullName = '' + this.data.FirstName + ' ' + this.data.MiddleName + ' ' + this.data.LastName;
                this.courses = this.data.Courses.toString();
            })
            .catch(error =>{
                this.errorMessage = error.response.data
            })
            
        },
    },
    beforeMount(){
        this.userId = sessionStorage.SITuserId;
        this.getUserProfile();
    },
}
</script>