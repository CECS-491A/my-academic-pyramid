
<template>
  <div id="schoolRegistration">
    <v-app id="schoolRegistration">
      <v-card>
        <v-toolbar>
          <v-toolbar-title>School Registration</v-toolbar-title>
        </v-toolbar>
        <v-card-title primary-title>
          <div>
            <h3 class="headline mb-0">Import JSON File</h3>
          </div>
        </v-card-title>
        <FileReader @load="text = $event"></FileReader>
        <v-btn @click="importData" color="light-blue">Import Data</v-btn>

        <v-card-text>
          <form>
            <v-text-field
              id="schoolName"
              v-model="schoolData.schoolDTO.SchoolName"
              :error-messages="textErrors"
              label="School Name"
              required
              @input="$v.schoolData.schoolDTO.SchoolName.$touch()"
              @blur="$v.schoolData.schoolDTO.SchoolName.$touch()"
            ></v-text-field>

            <v-text-field
              id="schoolContactEmail"
              v-model="schoolData.schoolDTO.SchoolContactEmail"
              :error-messages="emailErrors"
              label="School Contact Email"
              required
              @input="$v.schoolData.schoolDTO.SchoolContactEmail.$touch()"
              @blur="$v.schoolData.schoolDTO.SchoolContactEmail.$touch()"
            ></v-text-field>

            <v-text-field
              id="schoolEmailDomain"
              v-model="schoolData.schoolDTO.SchoolEmailDomain"
              :error-messages="domainErrors"
              label="School Email Domain"
              required
              @input="$v.schoolData.schoolDTO.SchoolEmailDomain.$touch()"
              @blur="$v.schoolData.schoolDTO.SchoolEmailDomain.$touch()"
            ></v-text-field>
          </form>
        </v-card-text>
        <v-alert
          v-model="departmentError"
          type="error"
          transition="scale-transition"
        >Please enter departments</v-alert>
        <departmentTable></departmentTable>

        <v-alert
          v-model="courseError"
          type="error"
          transition="scale-transition"
        >Please enter courses</v-alert>
        <courseTable></courseTable>

        <v-alert
          v-model="teacherError"
          type="error"
          transition="scale-transition"
        >Please enter teachers</v-alert>
        <teacherTable></teacherTable>
        <v-alert :value="failedAlert" type="error" transition="scale-transition">{{errors}}</v-alert>
        <v-alert
          :value="successAlert"
          type="success"
          transition="scale-transition"
        >School is registered Sucessfully</v-alert>

        <v-btn @click="RegisterSchool">submit</v-btn>
      </v-card>
    </v-app>
  </div>
</template>



<script>
/*global console*/ /* eslint no-console: "off" */
import { validationMixin } from "vuelidate";

import { required, maxLength, email, helpers } from "vuelidate/lib/validators";
import courseTable from "@/components/SchoolRegistration/CourseTable";
import teacherTable from "@/components/SchoolRegistration/TeacherTable";
import departmentTable from "@/components/SchoolRegistration/DepartmentTable";
import FileReader from "@/components/SchoolRegistration/FileReader";

const domainValidation = helpers.regex(
  "alpha",
  /(?:[a-z0-9](?:[a-z0-9-]{0,61}[a-z0-9])?\.)+[a-z0-9][a-z0-9-]{0,61}[a-z0-9]/
);
export default {
  mixins: [validationMixin],

  validations: {
    schoolData: {
      schoolDTO: {
        SchoolName: { required, maxLength: maxLength(100) },
        SchoolContactEmail: { required, email },
        SchoolEmailDomain: { required, domainValidation }
      }
    }
  },
  components: {
    courseTable,
    teacherTable,
    departmentTable,
    FileReader
  },

  data: () => ({
    schoolData: {
      schoolDTO: {
        SchoolName: "",
        SchoolContactEmail: "",
        SchoolEmailDomain: ""
      },

      departmentDTO: [],

      CourseDTO: [],

      TeacherDTO: []
    },

    text: "",
    errors: "",
    departmentError: false,
    courseError: false,
    teacherError: false,
    failedAlert: false,
    successAlert: false
  }),

  computed: {
    textErrors() {
      const errors = [];

      if (!this.$v.schoolData.schoolDTO.SchoolName.$dirty) return errors;

      !this.$v.schoolData.schoolDTO.SchoolName.maxLength &&
        errors.push("Name must be at most 10 characters long");

      !this.$v.schoolData.schoolDTO.SchoolName.required &&
        errors.push("Last Name is required.");

      return errors;
    },

    emailErrors() {
      const errors = [];

      if (!this.$v.schoolData.schoolDTO.SchoolContactEmail.$dirty)
        return errors;

      !this.$v.schoolData.schoolDTO.SchoolContactEmail.email &&
        errors.push("Must be valid e-mail");

      !this.$v.schoolData.schoolDTO.SchoolContactEmail.required &&
        errors.push("E-mail is required");

      return errors;
    },
    domainErrors() {
      const errors = [];

      if (!this.$v.schoolData.schoolDTO.SchoolEmailDomain.$dirty) return errors;

      !this.$v.schoolData.schoolDTO.SchoolEmailDomain.domainValidation &&
        errors.push("Must be valid domain");

      !this.$v.schoolData.schoolDTO.SchoolEmailDomain.required &&
        errors.push("Domain is required");

      return errors;
    }
  },
  created() {
    this.$eventBus.$on("SendDepartmentTable", departments => {
      this.schoolData.departmentDTO = departments;
      /* eslint no-console: "off" */
      console.log("Receive department");
    });

    this.$eventBus.$on("SendCourseTable", courses => {
      this.schoolData.CourseDTO = courses;
      /* eslint no-console: "off" */
      console.log("Receive crouse");
    }),
      this.$eventBus.$on("SendTeacherTable", teachers => {
        this.schoolData.TeacherDTO = teachers;
      });
  },
  watch: {
    schoolData() {
      this.$eventBus.$emit("TeacherTableFromFile", this.schoolData.TeacherDTO),
        this.$eventBus.$emit(
          "DepartmentTableFromFile",
          this.schoolData.departmentDTO
        ),
        this.$eventBus.$emit("CourseTableFromFile", this.schoolData.CourseDTO),
        this.$eventBus.$emit(
          "TeacherTableFromFile",
          this.schoolData.TeacherDTO
        );
    },
    "schoolData.departmentDTO": function() {
      if (this.schoolData.departmentDTO.length) {
        this.departmentError = false;
      }
    },
    "schoolData.CourseDTO": function() {
      if (this.schoolData.CourseDTO.length) {
        this.courseError = false;
      }
    },
    "schoolData.TeacherDTO": function() {
      if (this.schoolData.TeacherDTO.length) {
        this.teacherError = false;
      }
    }
  },
  methods: {
    tableValidation() {
      if (!this.schoolData.departmentDTO.length) {
        this.departmentError = true;
      }
      if (!this.schoolData.CourseDTO.length) {
        this.courseError = true;
      }
      if (!this.schoolData.TeacherDTO.length) {
        this.teacherError = true;
      }
    },
    convertData() {
      this.schoolData = JSON.parse(this.text);
    },
    importData() {
      this.schoolData = JSON.parse(this.text);
    },
    RegisterSchool() {
      this.tableValidation();
      this.$v.$touch();
      if (
        this.$v.$invalid ||
        !this.schoolData.departmentDTO.length ||
        !this.schoolData.CourseDTO.length ||
        !this.schoolData.TeacherDTO.length
      ) {
        (this.errors = "Submit Failed. Please check all fields"),
          (this.failedAlert = true);
      } else {
        this.axios({
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
            Authorization: "Bearer " + sessionStorage.SITtoken
          },
          method: "POST",
          crossDomain: true,
          url: this.$hostname + "SchoolRegistration",
          data: this.schoolData
        })
          .then(() => {
			this.failedAlert = false;
            this.successAlert = true;
          })

          .catch(err => {
            if (err.response.status == 409) {
              this.errors = "School with same name exists";
              this.failedAlert = true;
            }
          });
      }
    }
  }
};
</script>

