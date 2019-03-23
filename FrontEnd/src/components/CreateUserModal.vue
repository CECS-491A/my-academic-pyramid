<script>
/*global console*/ /* eslint no-console: "off" */
export default {
  name: "CreateModal",
  data() {
    return {
      formData: {
        UserName: "",
        FirstName: "",
        LastName: ""
      },
      response: ""
    };
  },
  methods: {
    close() {
      this.$emit("close");
    },
    submitData() {
      this.axios({
        method: "POST",
        crossDomain: true,
        url: this.$hostname,
        data: this.formData,
        headers: { "content-type": "application/json",
         "Access-Control-Allow-Origin": "*",
         "Access-Control-Allow-Methods": "GET, POST, PATCH, PUT, DELETE, OPTIONS",
    
        }
      }).then(
        result => {
          this.response = result.data;
        },
        error => {
          console.error(error);
        }
      );

      this.$eventBus.$emit("UpdateTable");
      this.$emit("close");
    }
  }
};
</script>

<template>
  <transition name="modal-fade">
    <div class="modal-backdrop">
      <div class="modal">
        <header class="modal-header">
          <slot name="header">
            Create UserName
            <button type="button" class="btn-close" @click="close">x</button>
          </slot>
        </header>
        <section class="modal-body">
          <slot name="body">
            <v-text-field id="UserName" label="UserName" v-model="formData.UserName"/>
            <br>
            <v-text-field id="firstName" label="First Name" v-model="formData.FirstName"/>
            <br>

            <v-text-field id="lastName" label="Last Name" v-model="formData.LastName"/>
            <br>
          </slot>
        </section>
        <footer class="modal-footer">
          <slot name="footer">
            <button type="button" class="btn-green" @click="submitData">Create User </button>
          </slot>
        </footer>
      </div>
    </div>
  </transition>
</template>

<style>
.modal-backdrop {
  position: fixed;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  background-color: rgba(0, 0, 0, 0.3);
  display: flex;
  justify-content: center;
  align-items: center;
}

.modal {
  background: #ffffff;
  box-shadow: 2px 2px 20px 1px;
  overflow-x: auto;
  display: flex;
  flex-direction: column;
}

.modal-header,
.modal-footer {
  padding: 15px;
  display: flex;
}

.modal-header {
  border-bottom: 1px solid #eeeeee;
  color: #4aae9b;
  justify-content: space-between;
}

.modal-footer {
  border-top: 1px solid #eeeeee;
  justify-content: flex-end;
}

.modal-body {
  position: relative;
  padding: 20px 10px;
}

.btn-close {
  border: none;
  font-size: 20px;
  padding: 20px;
  cursor: pointer;
  font-weight: bold;
  color: #4aae9b;
  background: transparent;
}

.btn-green {
  color: white;
  background: #4aae9b;
  border: 1px solid #4aae9b;
  border-radius: 2px;
}
</style>