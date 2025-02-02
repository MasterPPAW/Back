﻿using AutoMapper;

using LibrarieModele;
using LibrarieModele.DTOs;

namespace NivelAccesDate.Configs
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            CreateMap<Subscription, SubscriptionDTO>();
            CreateMap<SubscriptionDTO, Subscription>();

            CreateMap<WorkoutPlan, WorkoutPlanDTO>();
            CreateMap<WorkoutPlanDTO, WorkoutPlan>();

            CreateMap<Exercise, ExerciseDTO>();
            CreateMap<ExerciseDTO, Exercise>();

            CreateMap<Payment, PaymentDTO>();
            CreateMap<PaymentDTO, Payment>();

            CreateMap<WorkoutPlanExercise, WorkoutPlanExerciseDTO>();
            CreateMap<WorkoutPlanExerciseDTO, WorkoutPlanExercise>();
        }
    }
}
